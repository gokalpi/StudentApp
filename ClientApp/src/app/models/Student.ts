import { Address } from "./Address";

export interface Student {
  id?: number;
  name: string;
  email?: string;
  phone?: string;
  gender: string;
  bloodGroup: string;
  address: Address;
}
