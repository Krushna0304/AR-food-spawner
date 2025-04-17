#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <fcntl.h>
#include <string.h>
#include <sys/types.h>
#include <sys/wait.h>

#define MAX_LINE 1024

int main() {
    int pipe1[2], pipe2[2], pipe3[2];  
    pid_t child1, child2, child3;
    char line[MAX_LINE];
   
   
    if (pipe(pipe1) == -1 || pipe(pipe2) == -1 || pipe(pipe3) == -1) {
        perror("Pipe creation failed");
        exit(1);
    }
   
   
    child1 = fork();
    if (child1 < 0) {
        perror("Fork failed");
        exit(1);
    }
   
    if (child1 == 0) {  
   
        dup2(pipe1[1], STDOUT_FILENO);
       
   
        close(pipe1[0]); close(pipe1[1]);
        close(pipe2[0]); close(pipe2[1]);
        close(pipe3[0]); close(pipe3[1]);
       
       
        while (fgets(line, MAX_LINE, stdin) != NULL) {
            printf("%s", line);
            fflush(stdout);
        }
        exit(0);
    }
   
   
    child2 = fork();
    if (child2 < 0) {
        perror("Fork failed");
        exit(1);
    }
   
    if (child2 == 0) {  
       
        dup2(pipe1[0], STDIN_FILENO);
        dup2(pipe2[1], STDOUT_FILENO);
       
       
        close(pipe1[0]); close(pipe1[1]);
        close(pipe2[0]); close(pipe2[1]);
        close(pipe3[0]); close(pipe3[1]);
       
       
        execlp("sort", "sort", NULL);
        perror("sort failed");
        exit(1);
    }
   

    child3 = fork();
    if (child3 < 0) {
        perror("Fork failed");
        exit(1);
    }
   
    if (child3 == 0) {
       
        dup2(pipe2[0], STDIN_FILENO);
        dup2(pipe3[1], STDOUT_FILENO);
       
       
        close(pipe1[0]); close(pipe1[1]);
        close(pipe2[0]); close(pipe2[1]);
        close(pipe3[0]); close(pipe3[1]);
       
       
        execlp("uniq", "uniq", "-c", NULL);
        perror("uniq failed");
        exit(1);
    }
   
   
    close(pipe1[0]); close(pipe1[1]);
    close(pipe2[0]); close(pipe2[1]);
    close(pipe3[1]);
   
   
    int output_fd = open("122B1B247.txt", O_WRONLY | O_CREAT | O_TRUNC, 0644);
    if (output_fd == -1) {
        perror("Failed to open output file");
        exit(1);
    }
   
   
    printf("Enter text (Ctrl+D to finish):\n");
    fflush(stdout);
   
   
    waitpid(child1, NULL, 0);
   
   
    printf("\nResults:\n");
    fflush(stdout);
   
   
    char buffer[1024];
    ssize_t bytes_read;
   
    while ((bytes_read = read(pipe3[0], buffer, sizeof(buffer))) > 0) {
        write(output_fd, buffer, bytes_read);
        write(STDOUT_FILENO, buffer, bytes_read);
    }
   
   
    close(pipe3[0]);
    close(output_fd);
   
   
    waitpid(child2, NULL, 0);
    waitpid(child3, NULL, 0);
   
    printf("\nResults have been saved to 122B1B247.txt\n");
   
    return 0;
}