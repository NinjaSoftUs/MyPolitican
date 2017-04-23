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
  IndustriesTableComponent,
  SideLinksComponent
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
    IndustriesTableComponent,
    SideLinksComponent
    ],
    bootstrap:[MainComponent]
})
export class AppModule { }