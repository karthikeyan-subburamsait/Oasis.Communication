import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { EventTypeComponent } from './event-type/event-type.component';
import { LocalStorageService } from './service/local-storage.service';

@NgModule({
  declarations: [
    AppComponent,
    EventTypeComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [LocalStorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
