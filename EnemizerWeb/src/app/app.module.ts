import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { EnemizerFormComponent } from './enemizer-form/enemizer-form.component';
import { KeysPipe } from './keys.pipe';

@NgModule({
    declarations: [
        AppComponent,
        EnemizerFormComponent,
        KeysPipe
    ],
    imports: [
        BrowserModule,
        FormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
