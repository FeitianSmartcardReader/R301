#!/bin/bash
#
#Install Script for Linux and Mac OS X
#

echo
echo "Welcome To R301 install"
echo

if test $(id -ur) != 0; then
        echo
	echo "You should login as root user!"
	echo
	exit -1
fi

SYSOS=`uname -s`

if [ $SYSOS = "Linux" ]; then
######
	echo
	echo "Welcome To iR301-U For Linux"
	echo

#modify ccid info.plist  iR301-U
	if [ -f /usr/lib/pcsc/drivers/ifd-ccid.bundle/Contents/Info.plist ]; then
		declare -i start=0
	       	declare -i end=0
	        declare -i headline=0
	        declare -i endline=0
		start=`cat -n /usr/lib/pcsc/drivers/ifd-ccid.bundle/Contents/Info.plist | grep "FT iR301-U" | head -1 | cut -f1`
	        if [ $start = 0 ]; then
			cp /usr/lib/pcsc/drivers/ifd-ccid.bundle/Contents/Info.plist .
			cp Info.plist Info.plist.bak
       			grep "ifdVendorID" Info.plist.bak > /dev/null
                	if [ $? = 0 ]; then
                		start=`cat -n Info.plist.bak | grep "ifdVendorID" | head -1 | cut -f1`
                		end=`cat Info.plist.bak | wc -l`
                		headline=$start+1
                		endline=$end-$start-1
                		head -n $headline Info.plist.bak > Info.plist
				echo	"                <string>0x096E</string>"   >> Info.plist
                		tail -n $endline Info.plist.bak			    >> Info.plist
        		else
				echo "error: edit Info.plist"
			fi

			cp Info.plist Info.plist.bak
               		grep "ifdProductID" Info.plist.bak > /dev/null
	                if [ $? = 0 ]; then
       		         	start=`cat -n Info.plist.bak | grep "ifdProductID" | head -1 | cut -f1`
       		         	end=`cat Info.plist.bak | wc -l`
               		 	headline=$start+1
                		endline=$end-$start-1
                		head -n $headline Info.plist.bak > Info.plist
				echo	"                <string>0x0503</string>"   >> Info.plist
                		tail -n $endline Info.plist.bak			    >> Info.plist
        		else
				echo "error: edit Info.plist"
			fi

			cp Info.plist Info.plist.bak
               		grep "ifdFriendlyName" Info.plist.bak > /dev/null
                	if [ $? = 0 ]; then
                		start=`cat -n Info.plist.bak | grep "ifdFriendlyName" | head -1 | cut -f1`
                		end=`cat Info.plist.bak | wc -l`
                		headline=$start+1
                		endline=$end-$start-1
                		head -n $headline Info.plist.bak > Info.plist
				echo	"                <string>FT R301</string>"	>> Info.plist
                		tail -n $endline Info.plist.bak					>> Info.plist
        		else
				echo "error: edit Info.plist"
			fi
			cp Info.plist  /usr/lib/pcsc/drivers/ifd-ccid.bundle/Contents/Info.plist
			rm -rf Info.plist Info.plist.bak
		fi    
	else
		echo "not find ccid driver for pcsc lite"
		exit -1
	fi
	echo "install successful"
	exit 0
fi

SYSOS=`uname -s`

if [ $SYSOS = "Darwin" ]; then

######
	echo
	echo "Welcome To R301 For Mac OS X"
	echo

#modify ccid info.plist  iR301-U
	if [ -f /usr/libexec/SmartCardServices/drivers/ifd-ccid.bundle/Contents/Info.plist ]; then
		declare -i start=0
		declare -i end=0
		declare -i headline=0
		declare -i endline=0
		start=`cat -n /usr/libexec/SmartCardServices/drivers/ifd-ccid.bundle/Contents/Info.plist | grep "FT iR301-U" | head -1 | cut -f1`
		if [ $start = 0 ]; then
			cp /usr/libexec/SmartCardServices/drivers/ifd-ccid.bundle/Contents/Info.plist .
			cp Info.plist Info.plist.bak
		        grep "ifdVendorID" Info.plist.bak > /dev/null
		        if [ $? = 0 ]; then
		        	start=`cat -n Info.plist.bak | grep "ifdVendorID" | head -1 | cut -f1`
		        	end=`cat Info.plist.bak | wc -l`
		        	headline=$start+1
		        	endline=$end-$start-1
		        	head -n $headline Info.plist.bak > Info.plist
				echo	"                <string>0x096E</string>"   >> Info.plist
		        	tail -n $endline Info.plist.bak			    >> Info.plist
			else
				echo "error: edit Info.plist"
			fi

			cp Info.plist Info.plist.bak
		        grep "ifdProductID" Info.plist.bak > /dev/null
		        if [ $? = 0 ]; then
		        	start=`cat -n Info.plist.bak | grep "ifdProductID" | head -1 | cut -f1`
		        	end=`cat Info.plist.bak | wc -l`
		        	headline=$start+1
		        	endline=$end-$start-1
		        	head -n $headline Info.plist.bak > Info.plist
				echo	"                <string>0x0503</string>"   >> Info.plist
		        	tail -n $endline Info.plist.bak			    >> Info.plist
			else
				echo "error: edit Info.plist"
			fi

			cp Info.plist Info.plist.bak
		        grep "ifdFriendlyName" Info.plist.bak > /dev/null
		        if [ $? = 0 ]; then
		        	start=`cat -n Info.plist.bak | grep "ifdFriendlyName" | head -1 | cut -f1`
		        	end=`cat Info.plist.bak | wc -l`
		        	headline=$start+1
		        	endline=$end-$start-1
		        	head -n $headline Info.plist.bak > Info.plist
				echo	"                <string>FT R301</string>"	>> Info.plist
		        	tail -n $endline Info.plist.bak					>> Info.plist
			else
				echo "error: edit Info.plist"
			fi
			cp Info.plist  /usr/libexec/SmartCardServices/drivers/ifd-ccid.bundle/Contents/Info.plist
			rm -rf Info.plist Info.plist.bak
		fi    
	else
		echo "not find ccid driver for pcsc lite"
		exit -1
	fi

	echo "install successful"
	exit 0
fi
