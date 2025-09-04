import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CbtDemoPageComponent } from './cbt-demo-page.component';

describe('CbtDemoPageComponent', () => {
  let component: CbtDemoPageComponent;
  let fixture: ComponentFixture<CbtDemoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CbtDemoPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CbtDemoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
