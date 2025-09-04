import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ILAComponent } from './ila.component';

describe('ILAComponent', () => {
  let component: ILAComponent;
  let fixture: ComponentFixture<ILAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ILAComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ILAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
