import {
  ComponentPortal,
  ComponentType,
  Portal,
  TemplatePortal,
} from '@angular/cdk/portal';
import { Injectable, TemplateRef, ViewContainerRef } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { NavigationEnd, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { from, Subject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { sideBarBackDrop } from 'src/app/_Statemanagement/action/state.menutoggle';

@Injectable({
  providedIn: 'root',
})
export class FlyInPanelService {
  /** The panel. */
  panel: MatSidenav;
  main_menu: MatSidenav;
  private viewContainerRef: ViewContainerRef;
  private panelPortal$ = new Subject<Portal<any>>();

  constructor(private router: Router,private store: Store<{ toggle: string }>) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.close();
      });
  }

  /** Retrieves the current panel portal as an `Observable`. */
  get panelPortal() {
    return from(this.panelPortal$);
  }

  /** Sets the view container ref needed for {@link #setPanelContent}. */
  setViewContainerRef(vcr: ViewContainerRef) {
    this.viewContainerRef = vcr;
  }

  /** Sets the panel portal to the specified portal. */
  setPanelPortal(panelPortal: Portal<any>) {
    this.panelPortal$.next(panelPortal);
  }

  /**
   * Sets the panel content.
   * @param componentOrTemplateRef The component/template reference used.
   * @see PanelService#setPanelPortal
   */
  setPanelContent(
    componentOrTemplateRef: ComponentType<any> | TemplateRef<any>
  ) {
    let portal: Portal<any>;
    if (componentOrTemplateRef instanceof TemplateRef) {
      portal = new TemplatePortal(
        componentOrTemplateRef,
        this.viewContainerRef
      );
    } else {
      portal = new ComponentPortal(componentOrTemplateRef);
    }
    this.panelPortal$.next(portal);
  }

  /** Resets the current panel portal. */
  clearPanelPortal() {
    this.panelPortal$.next(undefined);
  }

  /** Opens the panel with optionally a portal to be set. */
  open(portal?: Portal<any>) {
    if (portal) {
      this.panelPortal$.next(portal);
    }
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    return this.panel.open();
  }

  /** Toggles the panel. */
  toggle() {
    return this.panel.toggle();
  }

  /** Closes the panel. */
  close() {
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    return this.panel.close();
  }
}
