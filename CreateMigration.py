#!/usr/bin/python

import sys
import subprocess

if len(sys.argv) < 2:
    print('Please supply a migration name')
    sys.exit(2)
    
subprocess.run(["dotnet", "tool", "restore"])
subprocess.run(["dotnet", "dotnet-ef", "migrations", "add", sys.argv[1], "--startup-project", "Forum.Presentation", "--project", "Forum.Persistence"])