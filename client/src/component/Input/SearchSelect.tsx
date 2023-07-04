'use client'

import { Container, Input, Select } from "@chakra-ui/react";
import { ChangeEvent, useState } from "react";

interface option {
  value:string,
  label:string
}
const options:option[] = [
  { value: 'apple', label: 'Apple' },
  { value: 'banana', label: 'Banana' },
  { value: 'orange', label: 'Orange' },
  { value: 'grape', label: 'Grape' },
  { value: 'mango', label: 'Mango' },
];

const selected:option = {
  value:"",
  label:""
}




function SearchSelect() {
  const [searchValue, setSearchValue] = useState('');
  const [selectedOption, setSelectedOption] = useState(selected);

  const filteredOptions = options.filter((option) =>
    option.label.toLowerCase().includes(searchValue.toLowerCase())
  );

  const handleSearchChange = (event:ChangeEvent<HTMLInputElement>) => {
    setSearchValue(event.target.value);
  };

  const handleSelectChange = (event:ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = event.target.value;
    const selectedOption = options.find((option) => option.value === selectedValue);
    
  };


  return (
    <Container m="0" p="0" position="relative" w="100%">
        <Select w="100%"/>
        <Input top="0" zIndex="1" position="absolute" w="90%" bg="transparent" borderColor="transparent" />
    </Container>
  )
}

  export default SearchSelect