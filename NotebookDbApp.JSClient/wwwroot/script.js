let notebooks = [];
let brands = [];
let cpus = [];
let gpus = [];
let connection = null;

let notebookIdToUpdate = -1;
let brandIdToUpdate = -1;
let cpuIdToUpdate = -1;
let gpuIdToUpdtae = -1;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:32747/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("NotebookCreated", (user, message) => {
        getdata();
    });
    connection.on("NotebookDeleted", (user, message) => {
        getdata();
    });
    connection.on("NotebookUpdated", (user, message) => {
        getdata();
    });

    connection.on("BrandCreated", (user, message) => {
        getdata();
    });
    connection.on("BrandDeleted", (user, message) => {
        getdata();
    });
    connection.on("BrandUpdated", (user, message) => {
        getdata();
    });

    connection.on("CpuCreated", (user, message) => {
        getdata();
    });
    connection.on("CpuDeleted", (user, message) => {
        getdata();
    });
    connection.on("CpuUpdated", (user, message) => {
        getdata();
    });

    connection.on("GpuCreated", (user, message) => {
        getdata();
    });
    connection.on("GpuDeleted", (user, message) => {
        getdata();
    });
    connection.on("GpuUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:32747/notebook')
        .then(x => x.json())
        .then(y => {
            notebooks = y;
            //console.log(notebooks);
            display();
        });
    await fetch('http://localhost:32747/brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(brands);
            display();
        });
    await fetch('http://localhost:32747/cpu')
        .then(x => x.json())
        .then(y => {
            cpus = y;
            console.log(cpus);
            display();
        });
    await fetch('http://localhost:32747/gpu')
        .then(x => x.json())
        .then(y => {
            gpus = y;
            console.log(gpus);
            display();
        });
}

function display() {
    document.getElementById('notebookresultarea').innerHTML = "";
    notebooks.forEach(t => {
        document.getElementById('notebookresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.brand.name + "</td><td>"
            + t.model + "</td><td>"
            + t.cpu.name + "</td><td>"
            + t.gpu.name + "</td><td>"
            + t.ram + "</td><td>"
            + t.storage + "</td><td>"
            + t.price + "</td><td>" +
        `<button type="button" onclick="removeNotebook(${t.id})">Delete</button>` +
        `<button type="button" onclick="showUpdateNotebook(${t.id})">Update</button>`
            + "</td></tr>";
    });

    document.getElementById('brandresultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('brandresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
        `<button type="button" onclick="removeBrand(${t.id})">Delete</button>` +
            `<button type="button" onclick="showUpdateBrand(${t.id})">Update</button>`
            + "</td></tr>";
    });

    document.getElementById('cpuresultarea').innerHTML = "";
    cpus.forEach(t => {
        document.getElementById('cpuresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>"
            + t.cores + "</td><td>"
            + t.threads + "</td><td>"
            + t.clockSpeed + "</td><td>" +
        `<button type="button" onclick="removeCpu(${t.id})">Delete</button>` + 
        `<button type="button" onclick="showUpdateCpu(${t.id})">Update</button>`
            + "</td></tr>";
    });

    document.getElementById('gpuresultarea').innerHTML = "";
    gpus.forEach(t => {
        document.getElementById('gpuresultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>"
            + t.memorySize + "</td><td>"
            + t.busWidth + "</td><td>" +
        `<button type="button" onclick="removeGpu(${t.id})">Delete</button>` +
        `<button type="button" onclick="showUpdateGpu(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function removeNotebook(id) {
    fetch('http://localhost:32747/notebook/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function removeBrand(id) {
    fetch('http://localhost:32747/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function removeCpu(id) {
    fetch('http://localhost:32747/cpu/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function removeGpu(id) {
    fetch('http://localhost:32747/gpu/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function createNotebook() {
    let brandId = document.getElementById('notebookbrandid').value;
    let model = document.getElementById('notebookmodel').value;
    let cpuId = document.getElementById('notebookcpuid').value;
    let gpuId = document.getElementById('notebookgpuid').value;
    let ram = document.getElementById('notebookram').value;
    let storage = document.getElementById('notebookstorage').value;
    let price = document.getElementById('notebookprice').value;

    fetch('http://localhost:32747/notebook', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                model: model,
                ram: ram,
                storage: storage,
                price: price,
                brandId: brandId,
                gpuId: gpuId,
                cpuId: cpuId
                })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function createBrand() {
    let name = document.getElementById('brandname').value;

    fetch('http://localhost:32747/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function createCpu() {
    let name = document.getElementById('cpuname').value;
    let cores = document.getElementById('cpucores').value;
    let threads = document.getElementById('cputhreads').value;
    let clockSpeed = document.getElementById('cpuclockspeed').value;

    fetch('http://localhost:32747/cpu', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                cores: cores,
                threads: threads,
                clockSpeed: clockSpeed
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function createGpu() {
    let name = document.getElementById('gpuname').value;
    let memorySize = document.getElementById('gpumemorysize').value;
    let busWidth = document.getElementById('gpubuswidth').value;

    fetch('http://localhost:32747/gpu', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                memorySize: memorySize,
                busWidth: busWidth
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showUpdateNotebook(id) {
    document.getElementById('notebookbrandidtoupdate').value = notebooks.find(t => t.id == id).brandId;
    document.getElementById('notebookmodeltoupdate').value = notebooks.find(t => t.id == id).model;
    document.getElementById('notebookcpuidtoupdate').value = notebooks.find(t => t.id == id).cpuId;
    document.getElementById('notebookgpuidtoupdate').value = notebooks.find(t => t.id == id).gpuId;
    document.getElementById('notebookramtoupdate').value = notebooks.find(t => t.id == id).ram;
    document.getElementById('notebookstoragetoupdate').value = notebooks.find(t => t.id == id).storage;
    document.getElementById('notebookpricetoupdate').value = notebooks.find(t => t.id == id).price;
    document.getElementById('notebookupdateformdiv').style.display = 'flex';
    notebookIdToUpdate = id;
}

function showUpdateBrand(id) {
    document.getElementById('brandnametoupdate').value = brands.find(t => t.id == id).name;
    document.getElementById('brandupdateformdiv').style.display = 'flex';
    brandIdToUpdate = id;
}

function showUpdateCpu(id) {
    document.getElementById('cpunametoupdate').value = cpus.find(t => t.id == id).name;
    document.getElementById('cpucorestoupdate').value = cpus.find(t => t.id == id).cores;
    document.getElementById('cputhreadstoupdate').value = cpus.find(t => t.id == id).threads;
    document.getElementById('cpuclockspeedtoupdate').value = cpus.find(t => t.id == id).clockSpeed;
    document.getElementById('cpuupdateformdiv').style.display = 'flex';
    cpuIdToUpdate = id;
}

function showUpdateGpu(id) {
    document.getElementById('gpunametoupdate').value = gpus.find(t => t.id == id).name;
    document.getElementById('gpumemorysizetoupdate').value = gpus.find(t => t.id == id).memorySize;
    document.getElementById('gpubuswidthtoupdate').value = gpus.find(t => t.id == id).busWidth;
    document.getElementById('gpuupdateformdiv').style.display = 'flex';
    gpuIdToUpdtae = id;
}


function updateNotebook() {
    document.getElementById('notebookupdateformdiv').style.display = 'none';
    let brandId = document.getElementById('notebookbrandidtoupdate').value;
    let model = document.getElementById('notebookmodeltoupdate').value;
    let cpuId = document.getElementById('notebookcpuidtoupdate').value;
    let gpuId = document.getElementById('notebookgpuidtoupdate').value;
    let ram = document.getElementById('notebookramtoupdate').value;
    let storage = document.getElementById('notebookstoragetoupdate').value;
    let price = document.getElementById('notebookpricetoupdate').value;

    fetch('http://localhost:32747/notebook', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: notebookIdToUpdate,
                model: model,
                ram: ram,
                storage: storage,
                price: price,
                brandId: brandId,
                gpuId: gpuId,
                cpuId: cpuId
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function updateBrand() {
    document.getElementById('brandupdateformdiv').style.display = 'none';
    let name = document.getElementById('brandnametoupdate').value;

    fetch('http://localhost:32747/brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: brandIdToUpdate,
                name: name
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function updateCpu() {
    document.getElementById('cpuupdateformdiv').style.display = 'none';
    let name = document.getElementById('cpunametoupdate').value;
    let cores = document.getElementById('cpucorestoupdate').value;
    let threads = document.getElementById('cputhreadstoupdate').value;
    let clockSpeed = document.getElementById('cpuclockspeedtoupdate').value;

    fetch('http://localhost:32747/cpu', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: cpuIdToUpdate,
                name: name,
                cores: cores,
                threads: threads,
                clockSpeed: clockSpeed
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function updateGpu() {
    document.getElementById('gpuupdateformdiv').style.display = 'none';
    let name = document.getElementById('gpunametoupdate').value;
    let memorySize = document.getElementById('gpumemorysizetoupdate').value;
    let busWidth = document.getElementById('gpubuswidthtoupdate').value;

    fetch('http://localhost:32747/gpu', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                id: gpuIdToUpdtae,
                name: name,
                memorySize: memorySize,
                busWidth: busWidth
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}