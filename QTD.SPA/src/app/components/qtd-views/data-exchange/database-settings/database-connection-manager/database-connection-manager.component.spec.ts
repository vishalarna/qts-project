import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatabaseConnectionManagerComponent } from './database-connection-manager.component';

describe('DatabaseConnectionManagerComponent', () => {
  let component: DatabaseConnectionManagerComponent;
  let fixture: ComponentFixture<DatabaseConnectionManagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatabaseConnectionManagerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatabaseConnectionManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
