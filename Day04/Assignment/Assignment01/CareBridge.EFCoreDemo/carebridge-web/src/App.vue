<script setup>
import { onMounted,ref } from 'vue'

const patients = ref([])
const city = ref('')


onMounted(() => {
  searchPatients()
})
// Function that runs when you click Search
const searchPatients = async () => {
  try {
    let url = 'https://localhost:7132/api/patients'

    
    if (city.value) {
      url += `?city=${encodeURIComponent(city.value)}`
    }
    
    const response = await fetch(url)
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }

    patients.value = await response.json()
  } catch (error) {
    console.error('Error fetching patients:', error)
  }
}

</script>

<template>

  <h1>CareBridge Patients</h1>
  
    <input v-model="city" placeholder="Enter city" />
    
    <button @click="searchPatients">Search</button>


  <table border="1">

    <tr>
      <th>Patient Id</th>
      <th>Full Name</th>
      <th>City</th>
      <th>Is Active </th>
    </tr>

    <!-- Loop through all patients -->

    <tr
      v-for="p in patients"
      :key="p.patientId">

      <td>{{ p.patientId }}</td>
      <td>{{ p.fullName }}</td>
      <td>{{ p.city }}</td>
      <td>{{ p.isActive }} </td>

    </tr>

  </table>

</template>
