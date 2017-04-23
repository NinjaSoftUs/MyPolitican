import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

import { 
  MainComponent,
  NavTopComponent,
  LeftSideNavComponent,
  MainPoliticanViewComponent,
  BlerbComponent,
  ContributorsTableComponent,
  IndustriesTableComponent
 } from './index';

@NgModule({
  imports: [
    CommonModule,
    BrowserModule    
  ],
  declarations: [
    MainComponent,
    NavTopComponent,
    LeftSideNavComponent,
    BlerbComponent,
    MainPoliticanViewComponent,
    ContributorsTableComponent,
    IndustriesTableComponent
    ],
    bootstrap:[MainComponent]
})
export class AppModule { }