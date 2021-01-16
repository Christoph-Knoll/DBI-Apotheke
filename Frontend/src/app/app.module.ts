import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import {RouterModule} from '@angular/router';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { AppRoutingModule } from './app-routing.module';
import { RedeemRecipeComponent } from './redeem-recipe/redeem-recipe.component';

@NgModule({
  declarations: [
    AppComponent,
    AddRecipeComponent,
    RedeemRecipeComponent
  ],
    imports: [
        BrowserModule,
        RouterModule,
        AppRoutingModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
