import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  // Properties
  title = 'Skinet';

  constructor(){ }

  ngOnInit(): void 
  {
  }

  // Life Cycle Hooks
}
