import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoTasksComponent } from './eo-tasks.component';

describe('EoTasksComponent', () => {
  let component: EoTasksComponent;
  let fixture: ComponentFixture<EoTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
