import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewDifResultsComponent } from './view-dif-results.component';

describe('ViewDifResultsComponent', () => {
  let component: ViewDifResultsComponent;
  let fixture: ComponentFixture<ViewDifResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewDifResultsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewDifResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
