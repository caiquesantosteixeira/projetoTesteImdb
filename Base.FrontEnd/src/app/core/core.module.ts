import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
// import { SidebarComponent } from './layout/sidebar/sidebar.component';
import { SidebarModule } from './components/layout/sidebar/sidebar.module';
import { NavbarModule } from './components/layout/navbar/navbar.module';
import { FooterModule } from './components/layout/footer/footer.module';
import { FixedpluginModule } from './components/layout/fixedplugin/fixedplugin.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    SidebarModule,
    NavbarModule,
    FooterModule,
    FixedpluginModule
  ],
  exports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    SidebarModule,
    NavbarModule,
    FooterModule,
    FixedpluginModule
  ]
})
export class CoreModule { }
