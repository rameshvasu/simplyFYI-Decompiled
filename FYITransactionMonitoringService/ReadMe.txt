[09022016]
EMERGENCY FIX: Enabled IPN POST activity logging.

---------------------------------------------------------------------------

[08022016]
EMERGENCY FIX: Stopped logging in FYIPayPalTransactionMonitorService.exe due to ASPNIX disk IO issue


[06202016 - Label:06202016_REV01_FYIPayPalTransactionMonitorService_PROD]
1. Added code to process only those accounts that are "enabled".
2. Added code to figre out the merchant who bought UNITS using value of payer_email.


[01062016 - Label:01062016_REV01_FYIPayPalTransactionMonitorService_PROD]
FYIPayPalTransactionMonitorService.exe -> Support to send confirmation email to merchant when simplyFYI UNITS are purchased.
---------------------------------------------------------------------------
[10052015 - Label:10052015_REV01_FYIPayPalTransactionMonitorService_PROD]
FYIPayPalTransactionMonitorService.exe -> Support for PayPal Send Money

[10052015 - Label:10052015_REV01_VoishareTypes_PROD]
VoishareTypes -> Support for PayPal Send Money

[10052015 - Label:10052015_REV01_Utils_PROD]
VoishareTypes -> Support for PayPal Send Money

-----------------------------------------------------------------------------
[07242015 - Label:07242015_REV01_Utils_PROD]
Utils.DLL -> Support for PayPal Virtual Terminal in PayPalHelper

[07242015 - Label:07242015_REV01_FYIPayPalTransactionMonitorService_PROD]
FYIPayPalTransactionMonitorService.exe -> Support for PayPal Virtual Terminal 

[07242015 - Label:07242015_REV01_VoishareTypes_PROD]
VoishareTypes -> Support for PayPal Virtual Terminal 

-------------------------------------------------------------------------------
[06302015 - Label:06302015_REV01_Utils_PROD]
Utils.DLL -> Emergency fix to correctly create line item details in Transaction Summary Table for cart transactions.

[06302015 - Label:06302015_REV01_FYIDAL_PROD]
FYIDataAccess.DLL -> Emergency fix to truncate Response to 2048 bytes before saving it to IPNTransactionDetail table.

[06302015 - Label:06302015_REV01_FYIPayPalTransactionMonitorService_PROD]
FYIPayPalTransactionMonitorService.EXE -> Emergency fix to identify and iterate through cart line items beyond 10 line items when
mapping from NVP to IPN format. Without this, cart transactions with more than 10 line items were failing.

--------------------------------------------------------------------------------

[01262015 - Label:01262015_REV01_Utils_PROD]
Updated: Utils.dll.
Description: Added support for shipping address tokens in message (PayPalHelper -> PersonalizeMessage().