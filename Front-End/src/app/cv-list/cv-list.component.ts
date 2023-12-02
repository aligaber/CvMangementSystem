import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { CvService } from '../cv.service';
import { ToastrService } from 'ngx-toastr';
import {ErrorStateMatcher} from '@angular/material/core';

interface IExperience_DTO{
  id: number | string,
  companyName: string,
  city: string,
  companyField: string,
  cvId: number| string
  }

interface ICV_ViewModel {
  id: number | string;
  name: string;
  fullname: string;
  cityname: string;
  email: string;
  mobilenumber: string;
  experienceInformation: IExperience_DTO[];
}

interface IPersonalInfo_DTO{
  id: number | undefined,
  fullname: string;
  cityname: string;
  email: string;
  mobilenumber: string;
}
interface ICV_DTO {
  id: number | string;
  name: string;
  personalInformation: IPersonalInfo_DTO
  experienceInformation: IExperience_DTO[];
}

const CV_viewModel: ICV_ViewModel = {
  id: "Cv Index",
  name: 'Name',
  fullname: 'Full Name',
  cityname: 'City Name',
  email: 'Email',
  mobilenumber: 'Mobile Number',
  experienceInformation: []
};

const Experiences_ViewModel : IExperience_DTO = {
  id: 'Experience Index',
  companyName: 'Company Name',
  city: 'City',
  companyField: 'Company Field',
  cvId: 'Cv Index'
}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-cv-list',
  templateUrl: './cv-list.component.html'
})
export class CvListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator?: MatPaginator;

  cvForm: FormGroup;
  isEditCV: boolean = false;
  modalRef!: BsModalRef;
  data: ICV_ViewModel[] = [];
  items: ICV_DTO[] = [];
  displayedPlaceholders: string[] = Object.values(CV_viewModel);
  displayedColumns: string[] = Object.keys(CV_viewModel);
  displayedExperiencesPlaceholders: string[] = Object.values(Experiences_ViewModel);
  displayedExperiencesColumns: string[] = Object.keys(Experiences_ViewModel);
  cvs = new MatTableDataSource<ICV_ViewModel>([]);
  experiences = new MatTableDataSource<IExperience_DTO>([]);
  currentPage: number = 1;
  pageSize: number = 10;
  pageSizeOptions: number[] = [];
  totalRows = 0;
  personalInformationForm: FormGroup;
  matcher = new MyErrorStateMatcher();
  cvIdToDelete = 0;
   //getters
   get cvFormControls() {
    return this.cvForm.controls;
  }

  get personalInformationFormControls() {
    return this.personalInformationForm.controls;
  }

  get experienceInformation() {
    return this.cvForm.controls["experienceInformation"] as FormArray;
  }

  constructor(
    private fb: FormBuilder, private modalService: BsModalService, private cvService: CvService, private toaster: ToastrService
  ) {
    this.personalInformationForm = this.fb.group({
      id: [0],
      fullname: [null, [Validators.required]],
      cityname: [null],
      email: [null, [Validators.email]],
      mobilenumber: [null, [Validators.required,Validators.pattern('^[0-9]*$')]],
      });
      
    this.cvForm = this.fb.group({
      id: [0],
      name: [null, [Validators.required]],
      personalInformation: this.personalInformationForm,
      experienceInformation: this.fb.array([])
    });


  }
 
  newExperience(): FormGroup { 
    return this.fb.group({
     id: [0], 
     companyName: ['', [Validators.required, Validators.maxLength(20)]],
     city: [''],
     companyField: [''],
     cvId: [0]
  });}

  ngOnInit(): void {
    this.getData();
    this.displayedColumns.push('actions');
    this.cvs.paginator = this.paginator as MatPaginator;
  }

  getData() {
    this.cvService.getCvsList(this.currentPage, this.pageSize).subscribe((res: any) => {
      this.totalRows = res.metaData.totalItemCount;

      if (this.paginator) {
        this.paginator.pageIndex = this.currentPage - 1;
        this.paginator.length = this.totalRows;
      }

      this.items = res.items;
      this.cvs = new MatTableDataSource(
        this.formatData(res.items)
      );
    });
    
  }

  formatData(data: any) {
    return data.map((item: any) => {
      return {
        id: item.id,
        name: item.name,
        fullname: item.personalInformation.fullName,
        cityname: item.personalInformation.cityName,
        email: item.personalInformation.email,
        mobilenumber: item.personalInformation.mobileNumber,
      };
    });
    
  }

  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage =  event.pageIndex;
    this.getData();
  }
  
  showAddCVPopup(template: TemplateRef<any>) {
    this.isEditCV = false;
    this.cvForm.reset();
    this.experienceInformation.clear();
    this.modalRef = this.modalService.show(template, {
      class: 'modal-style1 modal-dialog-centered',
      backdrop: true,
      ignoreBackdropClick: true
    });
  }

  addCV() {
    let cv:ICV_DTO = {
      id: 0,
      name: '',
      personalInformation:{
        id: 0,
        cityname: '',
        email: '',
        fullname: '',
        mobilenumber: ''
      },
      experienceInformation:[]
    };

    cv = this.cvForm.value;
    cv.id = 0;
    cv.personalInformation.id = 0;
    this.cvService.addCv(cv).subscribe(() => {
      this.modalRef.hide();
      this.getData();
      this.toaster.success("Addition successeded!");
    },() => this.toaster.error("problem occured while editig cv!", "Addition failed!"));
  }

  showEditCVPopup(row: any, template: TemplateRef<any>) {
    this.isEditCV = true;
    this.experienceInformation.clear();
    let cvData = this.items.find(x => x.id == row.id);
    let experiences = cvData?.experienceInformation;
    for (let i = 0;experiences && i < experiences.length; i++) {
      this.experienceInformation.push(this.fb.group({
        id: [experiences[i].id],
        cvId: [experiences[i].cvId],
        companyName: [experiences[i].companyName],
        city: [experiences[i].city],
        companyField: [experiences[i].companyField],
      }));
      this.experienceInformation.updateValueAndValidity();
    }

    this.cvFormControls['id'].setValue(row.id);
    this.cvFormControls['name'].setValue(row.name);
    this.personalInformationFormControls['id'].setValue(cvData?.personalInformation.id);
    this.personalInformationFormControls['fullname'].setValue(row.fullname);
    this.personalInformationFormControls['cityname'].setValue(row.cityname);
    this.personalInformationFormControls['email'].setValue(row.email);
    this.personalInformationFormControls['mobilenumber'].setValue(row.mobilenumber);
    
    this.modalRef = this.modalService.show(template, {
      class: 'modal-style1 modal-dialog-centered',
      backdrop: true,
      ignoreBackdropClick: true
    });
  }

  editCV() {
    let cv:ICV_DTO = {
      id: 0,
      name: '',
      personalInformation:{
        id: 0,
        cityname: '',
        email: '',
        fullname: '',
        mobilenumber: ''
      },
      experienceInformation:[]
    };
   
    cv = this.cvForm.value;
    this.cvService.editCv(cv).subscribe(() => {
      this.modalRef.hide();
      this.getData();
      this.toaster.success("Editing successeded!");
    },() => this.toaster.error("problem occured while editig cv!", "Editig failed!"));
    }

  showDeletionConfirmation(cvId: number,template: TemplateRef<any>) {
    this.cvIdToDelete = cvId;
    this.modalRef = this.modalService.show(template, {
      class: 'modal-style1 modal-dialog-centered',
      backdrop: true,
      ignoreBackdropClick: true
    });
  }

  deleteCv(){
     this.cvService.deleteCv(this.cvIdToDelete).subscribe(() => {
    this.getData();
    this.modalRef.hide();
    this.toaster.success("cv deleted successfully!", "Deletion successeded");
    },() => this.toaster.error("problem occured while deleting cv!", "Deletion failed!"));
  }

  showExperiences(row: any, template: TemplateRef<any>){
    let cv = this.items.find(x => x.id == row.id);
    this.experiences =  new MatTableDataSource<IExperience_DTO>(cv?.experienceInformation); 
    this.modalRef = this.modalService.show(template, {
      class: 'modal-style1 modal-dialog-centered',
      backdrop: true,
      ignoreBackdropClick: true
    });  
   
  }

  addExperience() {
    this.experienceInformation.push(this.newExperience());
  }

  deleteExperience(experienceIndex: number) {
    this.experienceInformation.removeAt(experienceIndex);
  }
}
