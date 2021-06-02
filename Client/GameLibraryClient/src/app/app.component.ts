import { APP_ID, Component } from '@angular/core';
import { GameAPIService } from './game-api.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  // title = 'GameLibraryClient';
  // public gameID: number = 1;


  constructor(private gameservice: GameAPIService){
  }


  // getGameByID(){
  // }
  
}
