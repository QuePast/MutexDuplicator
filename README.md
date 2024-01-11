# MutexDuplicator

## Overview
MutexDuplicator is a C# program designed for advanced users who need to manage and duplicate Mutex handles in a Windows environment. This utility facilitates Mutex handle management across different processes, providing insights and control over Mutex handles.

## Features
- Identify running processes related to specific applications (e.g., League of Legends) that use Mutex handles.
- Duplicate Mutex handles from one process to another (e.g., from a game process to a Python process).
- Improve debugging capabilities and gain better control over Mutex handles.
- Gracefully handle errors and provide informative error messages.

## Usage
1. Run the program to initiate Mutex handle duplication.
2. Specify the target processes you want to work with, such as "python.exe" and processes starting with "Leagueb."
