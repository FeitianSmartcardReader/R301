Run environment: JAVA
Building software: MyEclipse 2014 + VC6.00

Using jni technology



1. install MyEclipse 2014

2. Build DLL -> FTPCSC.dll
	run FTPCSC project to generate FTPCSC.dll
3. Import FTPCSCLoader.java + FTModule.java to your java project and copy FTPCSC.dll to java project, call FTPCSCLoader.java to operate smart card reader


To load demo project:
	Open MyEclipse -> import project into workspace, build and run