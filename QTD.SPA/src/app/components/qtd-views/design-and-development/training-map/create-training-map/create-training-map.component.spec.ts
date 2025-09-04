import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTrainingMapComponent } from './create-training-map.component';

describe('CreateTrainingMapComponent', () => {
  let component: CreateTrainingMapComponent;
  let fixture: ComponentFixture<CreateTrainingMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateTrainingMapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateTrainingMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
