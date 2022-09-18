echo off
cls
echo ÿ
echo This Batch file will print the Adventure Tutorial files
echo to the default printer port PRN:.
echo Make sure your printer is:
echo   -- turned on,
echo   -- positioned at the top of a page, and
echo   -- has at least 30 pages of paper ready to be printed.
echo ÿ
echo Press any key to begin (^Break to Abort)
echo ÿ
pause
echo ÿ
echo    Now printing . . . .
echo ÿ
TYPE NEWPAGE.PRT > PRN
echo ADVENT1.DOC
TYPE ADVENT1.DOC > PRN
TYPE NEWPAGE.PRT > PRN
echo ADVENT2.DOC
TYPE ADVENT2.DOC > PRN
TYPE NEWPAGE.PRT > PRN
echo ADVENT3.DOC
TYPE ADVENT3.DOC > PRN
TYPE NEWPAGE.PRT > PRN
echo ADVENT4.DOC
TYPE ADVENT4.DOC > PRN
TYPE NEWPAGE.PRT > PRN
echo ADVENT5.DOC
TYPE ADVENT5.DOC > PRN
TYPE NEWPAGE.PRT > PRN
echo ADVENT6.DOC
TYPE ADVENT6.DOC > PRN
TYPE NEWPAGE.PRT > PRN
echo APPLE.SCR
TYPE APPLE.SCR > PRN
TYPE NEWPAGE.PRT > PRN
echo COMM.SCR
TYPE COMM.SCR > PRN
TYPE NEWPAGE.PRT > PRN
echo IBM.SCR
TYPE IBM.SCR > PRN
TYPE NEWPAGE.PRT > PRN
echo RADIO.BAS
TYPE RADIO.BAS > PRN
TYPE NEWPAGE.PRT > PRN
TYPE NEWPAGE.PRT > PRN
echo That's all folks!
