import uWS from 'uWebSockets.js';


uWS.App().ws('/*',{
    message: (ws, message, isBinary) =>{
        ws.send(message,isBinary);
    }
}).listen(9001,(token)=>{
    if(token){
        console.log('DB Server Listening on port 9001');
    }else{
        console.log('Failed to listen on port 9001');
    }
});

