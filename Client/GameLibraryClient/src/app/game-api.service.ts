import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class GameAPIService {

  constructor(private _http: HttpClient) { }
  // public gameID : number = 1;
  // public fullgame : IFullGameInfo | undefined;

  // getGamebyID(){
  //   return this._http.get<IFullGameInfo>("https://localhost:44389/api/full/"+this.gameID);
  // }


}

// export interface IFullGameInfo{
//   game: IGame;
//   Tags: String; // TODO
// }

// export interface ITag{
//   id: number;
//   tagname: string;
// }
// export interface ITagsLink{
//   id: number;
//   tag : ITag;
//   game: IGame;
// }

// export interface IGame{
//   id: number;
//   Title: string;
//   Dev: IDevoloper;
// }
// export interface IDevoloper{
//   id: number;
//   DevName: string;
// }
