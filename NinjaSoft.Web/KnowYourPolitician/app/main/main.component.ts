import { Component, OnInit } from '@angular/core';
import{ LeftSideNavComponent } from '../index'

@Component({
  selector: 'app-main',
  templateUrl: 'app/main/main.component.html',
  styleUrls: ['app/main/main.component.css']
})
export class MainComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    console.log ("Main Component ngOnInit fired")
  }

}