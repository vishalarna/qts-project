import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolNavbarComponent } from './tool-navbar.component';

describe('ToolNavbarComponent', () => {
  let component: ToolNavbarComponent;
  let fixture: ComponentFixture<ToolNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToolNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
