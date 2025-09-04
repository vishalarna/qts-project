import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JtaNavBarComponent } from './jta-nav-bar.component';

describe('JtaNavBarComponent', () => {
  let component: JtaNavBarComponent;
  let fixture: ComponentFixture<JtaNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JtaNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JtaNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
