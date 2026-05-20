let suppliers = [];
let hangHoas = [];

// ==================== Initialization ====================

document.addEventListener('DOMContentLoaded', function() {
    initTabs();
    initEventListeners();
    loadInitialData();
});

function initTabs() {
    const tabBtns = document.querySelectorAll('.tab-btn');
    tabBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            switchTab(this.dataset.tab);
        });
    });
}

function initEventListeners() {
    // Add Form
    document.getElementById('addForm').addEventListener('submit', handleAddSubmit);

    // Search
    document.getElementById('searchBtn').addEventListener('click', handleSearch);

    // Edit Form
    document.getElementById('editForm').addEventListener('submit', handleEditSubmit);

    // Modal Close
    const modal = document.getElementById('editModal');
    const closeBtn = document.querySelector('.close');
    
    closeBtn.addEventListener('click', function() {
        modal.style.display = 'none';
    });

    window.addEventListener('click', function(event) {
        if (event.target == modal) {
            modal.style.display = 'none';
        }
    });
}

function switchTab(tabName) {
    // Hide all tabs
    document.querySelectorAll('.tab-content').forEach(el => {
        el.classList.remove('active');
    });

    // Remove active class from all buttons
    document.querySelectorAll('.tab-btn').forEach(el => {
        el.classList.remove('active');
    });

    // Show selected tab
    document.getElementById(tabName).classList.add('active');

    // Add active class to clicked button
    event.target.classList.add('active');

    // Load data based on tab
    if (tabName === 'list') {
        loadHangHoaList();
    } else if (tabName === 'suppliers') {
        loadSuppliersList();
    }
}

// ==================== Load Initial Data ====================

async function loadInitialData() {
    try {
        // Load suppliers first
        suppliers = await getNhaCungCaps();
        
        // Load hàng hóa
        hangHoas = await getHangHoas();
        
        // Populate supplier dropdowns
        populateSupplierDropdowns();
        
        // Load the list view
        loadHangHoaList();
        
        hideLoading();
    } catch (error) {
        console.error('Error loading initial data:', error);
        showAlert('Lỗi khi tải dữ liệu: ' + error.message, 'error');
    }
}

function populateSupplierDropdowns() {
    const addDropdown = document.getElementById('maNCC');
    const editDropdown = document.getElementById('editMaNCC');

    // Clear existing options
    addDropdown.innerHTML = '<option value="">-- Chọn nhà cung cấp --</option>';
    editDropdown.innerHTML = '';

    suppliers.forEach(supplier => {
        const option1 = document.createElement('option');
        option1.value = supplier.maNCC;
        option1.textContent = supplier.tenNCC;
        addDropdown.appendChild(option1);

        const option2 = document.createElement('option');
        option2.value = supplier.maNCC;
        option2.textContent = supplier.tenNCC;
        editDropdown.appendChild(option2);
    });
}

// ==================== Danh Sách Hàng Hóa ====================

async function loadHangHoaList() {
    try {
        hangHoas = await getHangHoas();
        renderHangHoaTable('hangHoaTable', hangHoas);
    } catch (error) {
        console.error('Error loading hàng hóa list:', error);
        showAlert('Lỗi khi tải danh sách: ' + error.message, 'error');
    }
}

function renderHangHoaTable(tableId, data) {
    const tbody = document.getElementById(tableId.replace('Table', 'Body'));
    tbody.innerHTML = '';

    if (data.length === 0) {
        tbody.innerHTML = '<tr><td colspan="7" style="text-align: center; color: #999;">Không có dữ liệu</td></tr>';
        return;
    }

    data.forEach(hangHoa => {
        tbody.innerHTML += createTableRow(hangHoa, suppliers);
    });
}

// ==================== Thêm Hàng Hóa ====================

async function handleAddSubmit(e) {
    e.preventDefault();

    if (!validateAddForm()) return;

    const data = {
        tenHang: document.getElementById('tenHang').value.trim(),
        donViTinh: document.getElementById('donViTinh').value,
        soLuongTon: parseInt(document.getElementById('soLuongTon').value),
        donGia: parseFloat(document.getElementById('donGia').value),
        maNCC: parseInt(document.getElementById('maNCC').value)
    };

    try {
        const result = await addHangHoa(data);
        showAlert('Thêm hàng hóa thành công!', 'success');
        
        // Reset form
        document.getElementById('addForm').reset();
        
        // Reload list
        await loadHangHoaList();
    } catch (error) {
        showAlert('Lỗi: ' + error.message, 'error');
    }
}

// ==================== Tìm Kiếm ====================

async function handleSearch(e) {
    e.preventDefault();

    const tenHang = document.getElementById('searchTenHang').value.trim();
    const donGiaMin = document.getElementById('searchDonGiaMin').value;
    const donGiaMax = document.getElementById('searchDonGiaMax').value;

    try {
        const results = await searchHangHoas(
            tenHang || null,
            donGiaMin ? parseFloat(donGiaMin) : null,
            donGiaMax ? parseFloat(donGiaMax) : null
        );

        renderHangHoaTable('searchResultsTable', results);
        showAlert('Tìm kiếm thành công!', 'success');
    } catch (error) {
        showAlert('Lỗi khi tìm kiếm: ' + error.message, 'error');
    }
}

// ==================== Sửa Hàng Hóa ====================

async function editHangHoa(id) {
    try {
        const hangHoa = await getHangHoaById(id);
        
        document.getElementById('editMaHang').value = hangHoa.maHang;
        document.getElementById('editTenHang').value = hangHoa.tenHang;
        document.getElementById('editDonViTinh').value = hangHoa.donViTinh;
        document.getElementById('editSoLuongTon').value = hangHoa.soLuongTon;
        document.getElementById('editDonGia').value = hangHoa.donGia;
        document.getElementById('editMaNCC').value = hangHoa.maNCC;

        document.getElementById('editModal').style.display = 'block';
    } catch (error) {
        showAlert('Lỗi khi tải dữ liệu: ' + error.message, 'error');
    }
}

async function handleEditSubmit(e) {
    e.preventDefault();

    if (!validateEditForm()) return;

    const id = parseInt(document.getElementById('editMaHang').value);
    const data = {
        tenHang: document.getElementById('editTenHang').value.trim(),
        donViTinh: document.getElementById('editDonViTinh').value,
        soLuongTon: parseInt(document.getElementById('editSoLuongTon').value),
        donGia: parseFloat(document.getElementById('editDonGia').value),
        maNCC: parseInt(document.getElementById('editMaNCC').value)
    };

    try {
        await updateHangHoa(id, data);
        showAlert('Cập nhật hàng hóa thành công!', 'success');
        
        document.getElementById('editModal').style.display = 'none';
        await loadHangHoaList();
    } catch (error) {
        showAlert('Lỗi: ' + error.message, 'error');
    }
}

// ==================== Xóa Hàng Hóa ====================

async function deleteHangHoaHandler(id, name) {
    if (!confirmDelete(name)) return;

    try {
        await deleteHangHoa(id);
        showAlert('Xóa hàng hóa thành công!', 'success');
        await loadHangHoaList();
    } catch (error) {
        showAlert('Lỗi: ' + error.message, 'error');
    }
}

// ==================== Danh Sách Nhà Cung Cấp ====================

async function loadSuppliersList() {
    try {
        suppliers = await getNhaCungCaps();
        renderSuppliersTable(suppliers);
    } catch (error) {
        console.error('Error loading suppliers list:', error);
        showAlert('Lỗi khi tải danh sách nhà cung cấp: ' + error.message, 'error');
    }
}

function renderSuppliersTable(data) {
    const tbody = document.getElementById('suppliersBody');
    tbody.innerHTML = '';

    if (data.length === 0) {
        tbody.innerHTML = '<tr><td colspan="4" style="text-align: center; color: #999;">Không có dữ liệu</td></tr>';
        return;
    }

    data.forEach(supplier => {
        tbody.innerHTML += createSupplierTableRow(supplier);
    });
}
