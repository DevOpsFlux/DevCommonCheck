<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 TRANSITIONAL//EN">
<html><head></head>
<body style="font-size:9pt">
[CheckTest] <br>
<%

	Dim objCom, bResult, ErrCode, ErrMsg, ErrOrderNum
	Dim Flag, ip, port
	Dim outErrCode, outErrMsg

	bResult = ""

	ip = request("i")
	port = request("p")

	If ip="" then 
		Response.Write "FAIL" & "<br/>"
		Response.end
	end if

	If port="" then
		port = "80"
	end if 

	port = CInt(port)

	On Error Resume Next

	Set objCom =  Server.CreateObject("DevCommonCheck.CheckLib")

	If Err.Number > 0 Then
		Response.Write "Fail"
		Response.Write "CreateObject ½ÇÆÐ : " & Err.Description
	Else		
		bResult = objCom.TelnetCheck(ip,port)
	
		'bResult = objCom.PingCheck("127.0.0.1")
		'bResult = objCom.PingCheck("190.xxx.xx.xxx")
		'bResult = objCom.PingCheck("210.xxx.xx.xx")

		'bResult = objCom.TelnetCheck("127.0.0.1",80)
		'bResult = objCom.TelnetCheck("190.xxx.xx.xxx",443)
		'bResult = objCom.TelnetCheck("210.xxx.xx.xx",443)
		'bResult = objCom.TelnetCheck("dev.flux.com",80)
		'bResult = objCom.TelnetCheck("dev.flux.com",443)

		If bResult Then
			Response.Write "SUCCESS" & "<br/>"
		Else
			Response.Write "FAIL" & "<br/>"
		End If	

	
	End If

	Call objCom.Dispose()
	objCom = Nothing

	On Error GoTo 0
%>

 </body>
</html>

	