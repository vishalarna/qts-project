import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DatabaseConnectionManagerComponent } from './database-connection-manager.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import {  MatTableModule } from '@angular/material/table';

@NgModule({
  declarations: [
    DatabaseConnectionManagerComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatTableModule
  ],
  exports: [
    DatabaseConnectionManagerComponent
  ]
})
export class DatabaseConnectionManagerModule { }
