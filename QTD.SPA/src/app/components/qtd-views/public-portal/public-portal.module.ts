import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicPortalRoutingModule } from './public-portal-routing.module';
import { PublicPortalComponent } from './public-portal.component';

@NgModule({
  declarations: [ PublicPortalComponent],
  imports: [
    CommonModule,
    PublicPortalRoutingModule
  ],
  exports:[PublicPortalComponent]
})
export class PublicPortalModule { }