import { Room, Client } from "colyseus";
import { State, Player } from "../schema/State";

export class WASDRoom extends Room<State> {
  maxClients = 64;

  onCreate(options: any) {
    this.setState(new State());

    this.onMessage("setPos", (client, data: { x: number; y: number; z: number }) => {
      const player = this.state.players.get(client.sessionId);
      if (player) {
        player.x = data.x;
        player.y = data.y;
        player.z = data.z;
      }
    });
  }

  onJoin(client: Client) {
    const player = new Player();
    this.state.players.set(client.sessionId, player);
    console.log(`🟢 ${client.sessionId} joined`);
  }

  onLeave(client: Client) {
    this.state.players.delete(client.sessionId);
    console.log(`🔴 ${client.sessionId} left`);
  }

  onDispose() {
    console.log(`Room ${this.roomId} disposed`);
  }
}
