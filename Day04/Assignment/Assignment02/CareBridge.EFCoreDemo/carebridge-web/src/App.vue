<script setup>
import { onMounted,ref } from 'vue'

const departments = ref([])
const fromDate = ref(60)


onMounted(() => {
  loadDepartments()
})
// Function that runs when you click Search
const loadDepartments = async () => {
  try {
    let url = `https://localhost:7132/api/analytics/department-load?days=${fromDate.value}`

    
    
    
    const response = await fetch(url)
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }

    departments.value = await response.json()
  } catch (error) {
    console.error('Error fetching departments:', error)
  }
}

</script>

<template>

  <h1>CareBridge Encounters Dashboard in each Department</h1>
  
    <input type="number" v-model="fromDate" /> 

    <button @click="loadDepartments">Get Analytics</button>
  <table border="1">

    <tr>
      <th>Department Name</th>
      <th>InPatient</th>
      <th>OutPatient</th>
      <th>ED </th>
      <th>Total</th>
    </tr>

    <!-- Loop through all Departments -->

    <tr
      v-for="(d,index) in departments"
      :key="d.departmentName"
      :style="index === 0 ? 'background-color: yellow; font-weight: bold;' : ''">

      <td>{{ d.departmentName }}</td>
      <td>{{ d.inpatient }}</td>
      <td>{{ d.outpatient }}</td>
      <td>{{ d.ed }} </td>
      <td>{{ d.total }} </td>
    </tr>

  </table>

</template>
