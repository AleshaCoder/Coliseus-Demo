import { Server } from "colyseus";
import { createServer } from "http";
import { WASDRoom } from "./rooms/WASDRoom";

const port = Number(process.env.PORT || 2567);
const gameServer = new Server({
  server: createServer(),
});

gameServer.define("wasd-room", WASDRoom);

gameServer.listen(port);
console.log(`[Colyseus] Listening on ws://localhost:${port}`);
