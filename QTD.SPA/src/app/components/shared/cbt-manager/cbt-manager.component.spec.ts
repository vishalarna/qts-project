import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CbtManagerComponent } from './cbt-manager.component';

describe('CbtManagerComponent', () => {
  let component: CbtManagerComponent;
  let fixture: ComponentFixture<CbtManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CbtManagerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CbtManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
