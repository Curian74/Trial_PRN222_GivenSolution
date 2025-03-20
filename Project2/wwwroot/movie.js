var connection = new signalR.HubConnectionBuilder().withUrl("/moviehub").build();

connection.on("ReceiveMessage", () => {
    location.reload();
})

connection.start().then().catch(err => console.error(err.toString()));