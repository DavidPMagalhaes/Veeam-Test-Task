# Veeam-Test-Task
Internal Development VEEAM Test Task

## Task:
- Please implement a program that synchronizes two folders: source and replica.
- The program should maintain a full, identical copy of source folder at replica
folder.
- Solve the test task by writing a program in C#.

## Requirements:
- Synchronization must be one-way: after the synchronization content of the replica
folder should be modified to exactly match content of the source folder;
- Synchronization should be performed periodically;
- File creation/copying/removal operations should be logged to a file and to the
console output;
- Folder paths, synchronization interval and log file path should be provided using
the command line arguments;
- It is undesirable to use third-party libraries that implement folder synchronization;
- It is allowed (and recommended) to use external libraries implementing other well-
known algorithms. For example, there is no point in implementing yet
another function that calculates MD5 if you need it for the task – it is perfectly
acceptable to use a third-party (or built-in) library;
- The solution should be presented in the form of a link to the public GitHub repository.

## Design/Pseudo Code:
### - Any new or updated Source files will be copied to the target directory
### - Any new Source folders will be copied to the target directory
1. Define source directory/path
2. Define target directory/path
3. Scan source directory/path
4. For each file in source:
    - Check if file exists in target
        - If it does, check if source is newer than target, if so copy from source to target
        - If it doesn't copy from the source to the target
5. Log the results to a file and to the console