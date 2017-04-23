import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-left-side-nav',
  templateUrl: 'app/componets/left-side-nav/left-side-nav.component.html',
  styleUrls: ['app/componets/left-side-nav/left-side-nav.component.css']
})
export class LeftSideNavComponent implements OnInit {

  sortTopToBottom:boolean;
  sortAz:boolean;
  showRepbulican:boolean;
  showDemocrat:boolean;
  showAll:boolean;
  showHouse:boolean;
  showSenate:boolean;
  showHandS:boolean;
  showCurentCycle:boolean;
  showAllCycles:boolean;

  constructor() { 
    this.sortTopToBottom = true;
    this.showAll = true;
    this.showAllCycles = true;
  }

  ngOnInit() {
  }

}