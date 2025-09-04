import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-error-pages',
  templateUrl: './error-pages.component.html',
  styleUrls: ['./error-pages.component.scss'],
})
export class ErrorPagesComponent implements OnInit {
  constructor(
    private _router: Router,
    private _activatedRoute: ActivatedRoute
  ) {
    
  }

  ngOnInit(): void {
    let page = this._activatedRoute.snapshot.data;
    if (page) this._router.navigate(['error/404']);
  }
}
