from socket import *
import time
serverPort = 5060
serverSocket = socket(AF_INET, SOCK_DGRAM)

serverSocket.bind(('', serverPort))

message, clientAddress = serverSocket.recvfrom(2048)
## after recieving close so that the tcp socket can bind
serverSocket.close()
#made tcp server 
TcpserverSocket = socket(AF_INET, SOCK_STREAM)
TcpserverSocket.bind(('', serverPort))
TcpserverSocket.listen(1)
#sending confirmation and telling them the address of the host
serverSocket.sendto(b'tcp server is ready', clientAddress)
time.sleep(0.4)
serverSocket.sendto(b'tcp server is ready', clientAddress)
time.sleep(0.4)
serverSocket.sendto(b'tcp server is ready', clientAddress)

while True:
    # connection socket has 2 other parameters, client 
    # ip and port if the client ip is different but same port it makes a new socket
    #
    connectionSocket, addr = TcpserverSocket.accept()
    sentence = connectionSocket.recv(1024).decode()
    
    # connectionSocket.send(capitalizedSentence.encode())
    connectionSocket.close()