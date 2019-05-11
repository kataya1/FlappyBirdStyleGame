from socket import *
import time
import threading

# serverName = 'localhost' # 'hostname' # localhost
serverPort = 5060
clientSocket = socket(AF_INET, SOCK_DGRAM)
#SOL_SOCKET something about it beeing in the socket layer 
# So_broadcast configure a scoket to send broadcast data
clientSocket.setsockopt(SOL_SOCKET, SO_BROADCAST,1)
message = b"hi i'm a udp client want to connect through tcp"
#we don't know the host's adress but we know the port so we broadcast
'''
class ping (threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)
    def run(self):
        time.sleep(0.4)
        clientSocket.sendto(message,('192.168.1.255',serverPort))'''
# pinging = ping()
# pinging.start()

#to solve the udp relibility issue
clientSocket.sendto(message,('192.168.1.255',5060))
time.sleep(0.4)
clientSocket.sendto(message,('192.168.1.255',5060))
time.sleep(0.4)
clientSocket.sendto(message,('192.168.1.255',5060))

# the client address is also attached, but it's done automaticly
# clientSocket.sendto(message.encode(),(serverName, serverPort))
modifiedMessage, serverAddress = clientSocket.recvfrom(2048)
# print(modifiedMessage.decode())
clientSocket.close()

TcpclientSocket = socket(AF_INET, SOCK_STREAM)
# use the data sent 
try:
    TcpclientSocket.connect(serverAddress)
except:
    print("failed to connect")
    TcpclientSocket.close()
#recieving data
modifiedSentence = TcpclientSocket.recv(1024)

if(modifiedSentence == "COMMAND:CLOSE-CONNECTION"):
    {
        TcpclientSocket.close()
    }

# print(modifiedMessage ,serverAddress)