import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShNavBarComponent } from './sh-nav-bar.component';

describe('ShNavBarComponent', () => {
  let component: ShNavBarComponent;
  let fixture: ComponentFixture<ShNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
