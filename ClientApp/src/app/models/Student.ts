import { Address } from "./Address";

export interface Student {
  Id: number;
  Name: string;
  Email?: string;
  Phone?: string;
  Gender: string;
  BloodGroup: string;
  Address: Address;
}
