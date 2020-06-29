import { Component } from '@angular/core';
import { MessageService } from "../../services";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private service: MessageService) {
  }

}
