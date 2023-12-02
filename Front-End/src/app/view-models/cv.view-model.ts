
interface ICV_ViewModel {
  id: number | null;
  name: string;
  fullname: string;
  cityname: string;
  email: string;
  mobilenumber: string;
  experiences: IExperience_ViewModel[]
}