import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShDetailTabComponent } from './sh-detail-tab.component';

describe('ShDetailTabComponent', () => {
  let component: ShDetailTabComponent;
  let fixture: ComponentFixture<ShDetailTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShDetailTabComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShDetailTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
