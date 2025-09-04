import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-training-map-landing',
  templateUrl: './training-map-landing.component.html',
  styleUrls: ['./training-map-landing.component.scss'],
})
export class TrainingMapLandingComponent implements OnInit {
  constructor(private _router: Router) {}

  ngOnInit(): void {}
  CreateTM() {
    this._router.navigate(['/dnd/trainingmap/create']);
  }
}
