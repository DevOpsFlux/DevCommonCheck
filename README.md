# DevCommonCheck Com+ 
내/외부 서버 통신 방화벽 체크

## 1. 프로젝트 정보 및 버젼

### *[ DevCommonCheck Solution ]	
### *[ DevCommonCheck.csproj ]	

| 프로젝트 | 설명 | .NET버젼 | AdoToFormats버젼 |
| -------- | -------- | -------- | -------- |
| DevCommonCheck | IIS Log File 검색	| .NET 3.5	| DevCommonCheck 1.0.0.0 |

## 2. DevCommonCheck 정보 및 참조
- Server Ping Check
- Server Firewall IP/Domain Port Check
- UDP Client Message Send
- using System.EnterpriseServices;
- using System.Runtime.InteropServices;
- using System.Diagnostics;
- using System.Security.Principal;

## 3. DevCommonCheck 사용 정보
* * 구성요소서비스 > COM+ 응용프로그램 > DevCommonCheck.dll 컴포넌트 등록

## 4. DevCommonCheck CheckWeb
- CheckLib.asp

## 5. DevCommonCheck CheckLib Class :
```
class CheckLib
		PingCheck
		TelnetCheck
		UDPClient
		ShellExecute
		IsAdministrator
```

## 5. Unit Test Sample
```
Set objCom =  Server.CreateObject("DevCommonCheck.CheckLib")

# Server Ping Check
bResult = objCom.PingCheck(ip)
bResult = objCom.PingCheck("127.0.0.1")
bResult = objCom.PingCheck("190.xxx.xx.xxx")
bResult = objCom.PingCheck("210.xxx.xx.xx")

# Server Firewall IP/Domain Port Check
bResult = objCom.TelnetCheck(ip,port)
bResult = objCom.TelnetCheck("127.0.0.1",80)
bResult = objCom.TelnetCheck("190.xxx.xx.xxx",443)
bResult = objCom.TelnetCheck("210.xxx.xx.xx",443)
bResult = objCom.TelnetCheck("dev.flux.com",80)
bResult = objCom.TelnetCheck("dev.flux.com",443)

# UDP Send
'logPath = "" ' log=N		
logPath = "D:\Com\Common\send.log"
msg = "전송 메세지 데이터"

bResult = objCom.UDPClient(string ServerIP, string ServerPort, string SendMsg, string LogPath)
bResult = objCom.UDPClient("190.x.x.1","9097",msg,logPath)

```

![CryptoEncDec IMG](https://user-images.githubusercontent.com/49525161/56465899-3ff33280-6443-11e9-9f24-2bd25840c2c3.JPG)