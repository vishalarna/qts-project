import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaDetailsComponent } from './ila-details.component';

describe('IlaDetailsComponent', () => {
  let component: IlaDetailsComponent;
  let fixture: ComponentFixture<IlaDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
