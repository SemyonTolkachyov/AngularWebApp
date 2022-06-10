import { Component, OnInit, Input } from '@angular/core';
import {ScriptsService} from 'src/app/scripts/scripts.service';

@Component({
  selector: 'app-scripts',
  templateUrl: './scripts.component.html',
  styleUrls: ['./scripts.component.css']
})
export class ScriptsComponent implements OnInit {

  constructor(private service:ScriptsService) { }

  @Input() emp:any;
  EmployeeId:string;
  EmployeeName:string;
  Department:string;
  DateOfJoining:string;
  PhotoFileName:string;
  PhotoFilePath:string;

  ngOnInit(): void {
  }

  getAll(){
    this.service.getAll().subscribe(res=>{
      alert(res.toString());
    });
  }
  
  getSalaryMore10000(){
    this.service.getSalaryMore10000().subscribe(res=>{
      alert(res.toString());
    });
  }

  updateSalary(){
    this.service.updateSalary().subscribe(res=>{
      alert(res.toString());
    });
  }

  deleteOldEmployees(){
    this.service.deleteOldEmployees().subscribe(res=>{
      alert(res.toString());
    });
  }

  test(){
    this.service.test().subscribe(res=>{
      alert(res.toString());
    });
  }
}
