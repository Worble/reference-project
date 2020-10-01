#!/usr/bin/python

import subprocess
subprocess.run(["dotnet", "tool", "restore"])
subprocess.run(["dotnet", "dotnet-ef", "database", "update", "--project", "Forum.Presentation"])