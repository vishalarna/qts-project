import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RrNavBarComponent } from './rr-nav-bar.component';

describe('RrNavBarComponent', () => {
  let component: RrNavBarComponent;
  let fixture: ComponentFixture<RrNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RrNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RrNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
